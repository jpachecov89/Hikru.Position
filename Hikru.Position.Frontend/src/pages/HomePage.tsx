import React from 'react';
import PositionList from '../components/PositionList';

const HomePage: React.FC = () => {
  return (
    <div className='page-container'>
      <h1 className='page-title'>List of Positions</h1>
      <PositionList />
    </div>
  );
};

export default HomePage;