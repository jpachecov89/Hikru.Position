import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getPositionById } from '../services/api';
import { Position } from '../types/position';

const PositionPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [position, setPosition] = useState<Position | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchPosition = async () => {
      if (!id) return;

      setIsLoading(true);
      setError(null);

      try {
        const data = await getPositionById(id);
        setPosition(data);
      } catch (err) {
        setError('Error al cargar los detalles de la posición');
      } finally {
        setIsLoading(false);
      }
    };

    fetchPosition();
  }, [id]);

  if (isLoading) {
    return <p>Cargando detalles de la posición...</p>;
  }

  if (error) {
    return <p style={{ color: 'red' }}>{error}</p>;
  }

  if (!position) {
    return <p>No se encontró la posición.</p>;
  }

  return (
    <div>
      <h1>{position.title}</h1>
      <p>{position.description}</p>
      <p><strong>ID:</strong> {position.positionId}</p>
    </div>
  );
};

export default PositionPage;