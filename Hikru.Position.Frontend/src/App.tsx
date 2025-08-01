import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { PositionProvider } from './context/PositionContext';
import HomePage from './pages/HomePage';
import CreatePositionPage from './pages/CreatePositionPage';
import EditPositionPage from './pages/EditPositionPage';
import LoginPage from './pages/LoginPage';  
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
                    <CreatePositionPage />
                  </ProtectedRoute>
                }
              />
            <Route
                path="/positions/edit/:id"
                element={
                  <ProtectedRoute>
                    <EditPositionPage />
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
