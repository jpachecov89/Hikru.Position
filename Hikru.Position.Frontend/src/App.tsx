import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { PositionProvider } from './context/PositionContext';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import PositionPage from './pages/PositionPage';
import PositionForm from './components/PositionForm';
import NotFoundPage from './pages/NotFoundPage';
import ProtectedRoute from './components/ProtectedRoute';
import PublicOnlyRoute from './components/PublicOnlyRoute';
import './App.css';

function App() {
  return (
    <AuthProvider>
      <PositionProvider>
        <Router>
          <Routes>
            <Route
              path="/login"
              element={
                <PublicOnlyRoute>
                  <LoginPage />
                </PublicOnlyRoute>
              } />
            <Route
                path="/"
                element={
                  <ProtectedRoute>
                    <HomePage />
                  </ProtectedRoute>
                }
              />
            <Route
                path="/positions/new"
                element={
                  <ProtectedRoute>
                    <PositionForm />
                  </ProtectedRoute>
                }
              />
            <Route
                path="/positions/:id"
                element={
                  <ProtectedRoute>
                    <PositionPage />
                  </ProtectedRoute>
                }
              />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </Router>
      </PositionProvider>
    </AuthProvider>
  );
}

export default App;
