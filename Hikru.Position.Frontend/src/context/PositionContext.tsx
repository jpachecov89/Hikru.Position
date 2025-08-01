import React, { createContext, useState, useContext } from 'react';
import { Position } from '../types/position';

interface PositionContextProps {
  positions: Position[];
  setPositions: React.Dispatch<React.SetStateAction<Position[]>>;
}

const PositionContext = createContext<PositionContextProps | undefined>(undefined);

export const PositionProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [positions, setPositions] = useState<Position[]>([]);

  return (
    <PositionContext.Provider value={{ positions, setPositions }}>
      {children}
    </PositionContext.Provider>
  );
};

export const usePositionContext = () => {
  const context = useContext(PositionContext);
  if (!context) {
    throw new Error('usePositionContext must be used within a PositionProvider');
  }
  return context;
};