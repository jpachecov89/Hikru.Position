import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { createPosition, getDepartments, getRecruiters } from '../services/api';
import { PositionStatus, PositionStatusNames } from '../enums/positionStatus';
import { Recruiter } from '../types/recruiter';
import { Department } from '../types/department';
import { CreatePositionDto } from '../types/createPosition';

const PositionForm: React.FC = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState<CreatePositionDto>({
    title: '',
    description: '',
    location: '',
    status: PositionStatus.Draft,
    recruiterId: '',
    departmentId: '',
    budget: 0,
    closingDate: null,
  });

  const [error, setError] = useState('');
  const [recruiters, setRecruiters] = useState<Recruiter[]>([]);
  const [departments, setDepartments] = useState<Department[]>([]);

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
      [name]: name === 'budget' ? Number(value) : value
        ? Number(value)
        : name === 'closingDate' && value === ''
        ? null
        : name === 'status'
        ? parseInt(value)
        : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.title || !formData.description || !formData.location) {
      setError('Please fill in all required fields.');
      return;
    }

    try {
      await createPosition(formData);
      alert('Position created successfully');
      navigate('/');
    } catch (err) {
      console.error("Error creating position:", err);
      setError("Failed to create position. Please try again.");
    }
  };

  return (
    <div className="position-form-container">
      <h2>New Position</h2>
      {error && <p className="form-error">{error}</p>}

      <form onSubmit={handleSubmit} className="position-form">
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
          Budget
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
          <button type="submit" className="submit-button">Create</button>
          <button type="button" className="cancel-button" onClick={() => navigate('/')}>Cancel</button>
        </div>
      </form>
    </div>
  );
};

export default PositionForm;