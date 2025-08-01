export enum PositionStatus {
  Draft = 0,
  Open = 1,
  Closed = 2,
  Archived = 3,
}

export const PositionStatusNames: Record<PositionStatus, string> = {
  [PositionStatus.Draft]: "Draft",
  [PositionStatus.Open]: "Open",
  [PositionStatus.Closed]: "Closed",
  [PositionStatus.Archived]: "Archived",
};