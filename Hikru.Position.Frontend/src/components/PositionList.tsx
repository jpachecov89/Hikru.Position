import React, { useEffect, useState } from 'react';
import { usePositionContext } from '../context/PositionContext';
import { getPositions, deletePosition } from '../services/api';
import { PositionStatus, PositionStatusNames } from '../enums/positionStatus';
import { useNavigate } from 'react-router-dom';
import Modal from './Modal';

const PositionList: React.FC = () => {
  const { positions, setPositions } = usePositionContext();
  const [showModal, setShowModal] = useState(false);
  const [positionToDelete, setPositionToDelete] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPositions = async () => {
      const data = await getPositions();
      setPositions(data);
    };
    fetchPositions();
  }, [setPositions]);

  const handleDeleteRequest = async (id: string) => {
    setPositionToDelete(id);
    setShowModal(true);
  }

  const confirmDelete = async () => {
    if (!positionToDelete) return;

    try {
      await deletePosition(positionToDelete);
      setPositions((prev) => prev.filter((pos) => pos.positionId !== positionToDelete));
      alert('Position deleted successfully');
    } catch (error) {
      console.error('Error deleting position:', error);
      alert('Failed to delete position');
    } finally {
      setShowModal(false);
      setPositionToDelete(null);
    }
  }

  return (
    <div className="position-table-container">
      <div className="position-header">
        <h1>Open Positions</h1>
        <button className="new-position-button" onClick={() => navigate('/positions/new')}>
          + New Position
        </button>
      </div>
      <table className="position-table">
        <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Location</th>
            <th>Status</th>
            <th>Recruiter</th>
            <th>Department</th>
            <th>Budget</th>
            <th>Closing Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {positions.map((position) => (
            <tr key={position.positionId}>
              <td>{position.title}</td>
              <td>{position.description}</td>
              <td>{position.location}</td>
              <td>{PositionStatusNames[position.status as PositionStatus] ?? "Unknown"}</td>
              <td>{position.recruiter}</td>
              <td>{position.department}</td>
              <td>${position.budget.toLocaleString()}</td>
              <td>{position.closingDate ? new Date(position.closingDate).toLocaleDateString() : '—'}</td>
              <td>
                <div className="action-buttons">
                  <button className="edit-button">Edit</button>
                  <button className="delete-button" onClick={() => handleDeleteRequest(position.positionId)}>Delete</button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {showModal && (
        <Modal
          title="Confirm Delete"
          onClose={() => {
            setShowModal(false);
            setPositionToDelete(null);
          }}
          onConfirm={confirmDelete}
          confirmText="Delete"
          cancelText="Cancel"
        >
          <p>Are you sure you want to delete this position? This action cannot be undone.</p>
        </Modal>
      )}
    </div>
  );
};

export default PositionList;