import { useNavigate } from "react-router-dom";
import PositionForm from "../components/PositionForm";
import { CreatePositionDto } from "../types/createPosition";
import { createPosition } from "../services/api";
import { PositionStatus } from "../enums/positionStatus";

const CreatePositionPage = () => {
  const navigate = useNavigate();

  const handleCreate = async (data: CreatePositionDto) => {
    await createPosition(data);
    navigate('/');
  };

  return (
    <PositionForm
      initialData={{
        title: '',
        description: '',
        location: '',
        status: PositionStatus.Draft,
        recruiterId: '',
        departmentId: '',
        budget: 0,
        closingDate: null
      }}
      onSubmit={handleCreate}
    />
  );
};

export default CreatePositionPage;