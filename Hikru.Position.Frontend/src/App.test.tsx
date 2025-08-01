import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter, Route, Routes } from 'react-router-dom';
import App from './App';

jest.mock('./components/ProtectedRoute', () => ({ children }: any) => <>{children}</>);
jest.mock('./components/PublicOnlyRoute', () => ({ children }: any) => <>{children}</>);
jest.mock('./context/AuthContext', () => ({
  AuthProvider: ({ children }: any) => <>{children}</>
}));
jest.mock('./context/PositionContext', () => ({
  PositionProvider: ({ children }: any) => <>{children}</>
}));

describe('App routing', () => {
  test('renders home page on "/" route', () => {
    render(
      <MemoryRouter initialEntries={['/']}>
        <App />
      </MemoryRouter>
    );
    expect(screen.getByText(/Open Positions/i)).toBeInTheDocument();
  });

  test('renders login page on "/login"', () => {
    render(
      <MemoryRouter initialEntries={['/login']}>
        <App />
      </MemoryRouter>
    );
    expect(screen.getByText(/login/i)).toBeInTheDocument();
  });

  test('renders 404 page on unknown route', () => {
    render(
      <MemoryRouter initialEntries={['/non-existent-route']}>
        <App />
      </MemoryRouter>
    );
    expect(screen.getByText(/not found/i)).toBeInTheDocument();
  });
});