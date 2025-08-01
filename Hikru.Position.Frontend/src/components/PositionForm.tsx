import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getDepartments, getRecruiters } from '../services/api';
import { PositionStatus, PositionStatusNames } from '../enums/positionStatus';
import { Recruiter } from '../types/recruiter';
import { Department } from '../types/department';
import { CreatePositionDto } from '../types/createPosition';
import { UpdatePositionDto } from '../types/updatePosition';
import Modal from './Modal';

interface PositionFormProps {
  initialData: CreatePositionDto | UpdatePositionDto;
  onSubmit: (data: CreatePositionDto | UpdatePositionDto) => void;
  isEditMode?: boolean;
}

const PositionForm: React.FC<PositionFormProps> = ({ initialData, onSubmit, isEditMode = false }) => {
  const MAX_TITLE_LENGTH = 100;
  const MAX_DESCRIPTION_LENGTH = 1000;

  const navigate = useNavigate();

  const [formData, setFormData] = useState(initialData);
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [recruiters, setRecruiters] = useState<Recruiter[]>([]);
  const [departments, setDepartments] = useState<Department[]>([]);

  useEffect(() => {
    setFormData(initialData);
  }, [initialData]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [recruitersData, departmentsData] = await Promise.all([
          getRecruiters(),
          getDepartments()
        ]);
        setRecruiters(recruitersData);
        setDepartments(departmentsData);
      } catch (error) {
        console.error("Error fetching dropdown data:", error);
      }
    };
    fetchData();
  }, []);

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

  const handleSubmitRequest = async () => {
    if (!formData.title || !formData.description || !formData.location || !formData.recruiterId || !formData.departmentId || formData.budget <= 0) {
      setError('Please fill in all required fields.');
      return;
    }

    if (formData.title.length > MAX_TITLE_LENGTH) {
      setError(`Title must be ${MAX_TITLE_LENGTH} characters or less.`);
      return;
    }

    if (formData.description.length > MAX_DESCRIPTION_LENGTH) {
      setError(`Description must be ${MAX_DESCRIPTION_LENGTH} characters or less.`);
      return;
    }

    setError('');
    setShowModal(true);
  }

  const handleSubmit = async () => {
    try {
      await onSubmit(formData);
      alert(isEditMode ? 'Position updated successfully' : 'Position created successfully');
      navigate('/');
    } catch (err) {
      console.error("Error submitting position:", err);
      setError("Failed to process the request. Please try again.");
    } finally {
      setShowModal(false);
    }
  };

  return (
    <div className="position-form-container">
      <h2>{isEditMode ? 'Edit Position' : 'New Position'}</h2>
      {error && <p className="form-error">{error}</p>}

      <form className="position-form">
        <label>
          Title<span>*</span>
          <input type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
            maxLength={MAX_TITLE_LENGTH} />
        </label>

        <label>
          Description<span>*</span>
          <textarea name="description"
            value={formData.description}
            onChange={handleChange}
            required
            maxLength={MAX_DESCRIPTION_LENGTH} />
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
          <button type="button" className="submit-button" onClick={handleSubmitRequest}>
            {isEditMode ? 'Save' : 'Create'}
          </button>
          <button type="button" className="cancel-button" onClick={() => navigate('/')}>Cancel</button>
        </div>
      </form>

      {showModal && (
        <Modal
          title={isEditMode ? 'Confirm Update' : 'Confirm Creation'}
          onClose={() => setShowModal(false)}
          onConfirm={handleSubmit}
          confirmText={isEditMode ? 'Save' : 'Create'}
          cancelText="Cancel"
        >
          <p>{isEditMode
            ? 'Are you sure you want to update this position?'
            : 'Are you sure you want to create this position? This action cannot be undone.'}</p>
        </Modal>
      )}
    </div>
  );
};

export default PositionForm;