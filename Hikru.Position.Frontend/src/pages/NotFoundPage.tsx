import React from 'react';
import { Link } from 'react-router-dom';

const NotFoundPage: React.FC = () => {
  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h1>404 - Page Not Found</h1>
      <p>Sorry, we cannot find what you're looking for.</p>
      <Link to="/" style={{ color: 'blue', textDecoration: 'underline' }}>
        Home
      </Link>
    </div>
  );
};

export default NotFoundPage;