import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getPositionById, updatePosition, getDepartments, getRecruiters } from '../services/api';
import { PositionStatus, PositionStatusNames } from '../enums/positionStatus';
import { Recruiter } from '../types/recruiter';
import { Department } from '../types/department';
import { UpdatePositionDto } from '../types/updatePosition';
import Modal from './Modal';

const PositionDetails: React.FC = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState<UpdatePositionDto>({
    positionId: '',
    title: '',
    description: '',
    location: '',
    status: PositionStatus.Draft,
    recruiterId: '',
    departmentId: '',
    budget: 0,
    closingDate: null,
  });

  const [recruiters, setRecruiters] = useState<Recruiter[]>([]);
  const [departments, setDepartments] = useState<Department[]>([]);
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [recruitersData, departmentsData, positionData] = await Promise.all([
          getRecruiters(),
          getDepartments(),
          getPositionById(id!)
        ]);
        setRecruiters(recruitersData);
        setDepartments(departmentsData);

        setFormData({
          ...positionData,
          closingDate: positionData.closingDate
            ? new Date(positionData.closingDate).toISOString().split('T')[0]
            : null
        });
      } catch (err) {
        console.error("Error loading data:", err);
        setError("Failed to load position details.");
      }
    };

    fetchData();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]:
        name === 'budget' ? Number(value) :
        name === 'status' ? parseInt(value) :
        name === 'closingDate' && value === '' ? null :
        value
    }));
  };

  const handleSaveRequest = () => {
    if (!formData.title || !formData.description || !formData.location) {
      setError('Please fill in all required fields.');
      return;
    }
    setShowModal(true);
  };

  const handleSave = async () => {
    try {
      await updatePosition(id!, formData);
      alert('Position updated successfully');
      navigate('/');
    } catch (err) {
      console.error("Error updating position:", err);
      setError("Failed to update position. Please try again.");
    } finally {
      setShowModal(false);
    }
  };

  return (
    <div className="position-form-container">
      <h2>Edit Position</h2>
      {error && <p className="form-error">{error}</p>}

      <form className="position-form">
        <label>
          Title<span>*</span>
          <input type="text" name="title" value={formData.title} onChange={handleChange} required />
        </label>

        <label>
          Description<span>*</span>
          <textarea name="description" value={formData.description} onChange={handleChange} required />
        </label>

        <label>
          Location<span>*</span>
          <input type="text" name="location" value={formData.location} onChange={handleChange} required />
        </label>

        <label>
          Status
          <select name="status" value={formData.status} onChange={handleChange}>
            {Object.values(PositionStatus)
              .filter(value => typeof value === 'number')
              .map((value) => (
                <option key={value} value={value}>
                  {PositionStatusNames[value as PositionStatus]}
                </option>
              ))}
          </select>
        </label>

        <label>
          Recruiter<span>*</span>
          <select name="recruiterId" value={formData.recruiterId} onChange={handleChange} required>
            <option value="">-- Select Recruiter --</option>
            {recruiters.map(r => (
              <option key={r.recruiterId} value={r.recruiterId}>{r.name}</option>
            ))}
          </select>
        </label>

        <label>
          Department<span>*</span>
          <select name="departmentId" value={formData.departmentId} onChange={handleChange} required>
            <option value="">-- Select Department --</option>
            {departments.map(d => (
              <option key={d.departmentId} value={d.departmentId}>{d.name}</option>
            ))}
          </select>
        </label>

        <label>
          Budget<span>*</span>
          <input type="number" name="budget" value={formData.budget} onChange={handleChange} />
        </label>

        <label>
          Closing Date
          <input
            type="date"
            name="closingDate"
            value={formData.closingDate ? formData.closingDate : ''}
            onChange={handleChange}
          />
        </label>

        <div className="form-actions">
          <button type="button" className="submit-button" onClick={handleSaveRequest}>Save</button>
          <button type="button" className="cancel-button" onClick={() => navigate('/')}>Cancel</button>
        </div>
      </form>

      {showModal && (
        <Modal
          title="Confirm Update"
          onClose={() => setShowModal(false)}
          onConfirm={handleSave}
          confirmText="Save"
          cancelText="Cancel"
        >
          <p>Are you sure you want to update this position?</p>
        </Modal>
      )}
    </div>
  );
};

export default PositionDetails;