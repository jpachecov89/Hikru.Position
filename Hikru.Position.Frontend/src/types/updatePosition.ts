import { CreatePositionDto } from "./createPosition";

export interface UpdatePositionDto extends CreatePositionDto {
  positionId: string;
}