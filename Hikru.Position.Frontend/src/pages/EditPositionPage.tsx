import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getPositionById, updatePosition } from "../services/api";
import { UpdatePositionDto } from "../types/updatePosition";
import PositionForm from "../components/PositionForm";
import { CreatePositionDto } from "../types/createPosition";

const EditPositionPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<UpdatePositionDto | null>(null);

  useEffect(() => {
    if (!id) return;
    getPositionById(id!).then(position => {
      setData({
        ...position,
        closingDate: position.closingDate ? new Date(position.closingDate).toISOString().split('T')[0] : null
      });
    });
  }, [id]);

  const handleUpdate = async (data: CreatePositionDto | UpdatePositionDto) => {
    if (!('positionId' in data)) {
        console.error('Missing positionId in update data');
        return;
    }
    await updatePosition(data.positionId, data);
    navigate('/');
  };

  return data ? (
    <PositionForm
      initialData={data}
      onSubmit={handleUpdate}
      isEditMode
    />
  ) : <p>Loading...</p>;
};

export default EditPositionPage;