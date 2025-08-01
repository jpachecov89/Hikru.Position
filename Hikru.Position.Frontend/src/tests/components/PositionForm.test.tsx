import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import PositionForm from '../../components/PositionForm';

describe('PositionForm', () => {
  const initialData = {
    title: '',
    description: '',
    location: '',
    status: 0,
    recruiterId: '',
    departmentId: '',
    budget: 0,
    closingDate: null,
  };

  test('renders required fields', () => {
    render(<PositionForm initialData={initialData} onSubmit={jest.fn()} />);

    expect(screen.getByLabelText(/title/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/description/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/location/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/recruiter/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/department/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/budget/i)).toBeInTheDocument();
  });

  test('shows validation error if required fields are empty', async () => {
    render(<PositionForm initialData={initialData} onSubmit={jest.fn()} />);
    fireEvent.click(screen.getByText(/create/i));
    expect(await screen.findByText(/please fill in all required fields/i)).toBeInTheDocument();
  });

  test('submits form when valid and modal confirmed', async () => {
    const mockOnSubmit = jest.fn();

    render(<PositionForm initialData={initialData} onSubmit={mockOnSubmit} />);

    fireEvent.change(screen.getByLabelText(/title/i), { target: { value: 'Frontend Lead' } });
    fireEvent.change(screen.getByLabelText(/description/i), { target: { value: 'Build UI' } });
    fireEvent.change(screen.getByLabelText(/location/i), { target: { value: 'Lima' } });
    fireEvent.change(screen.getByLabelText(/budget/i), { target: { value: '5000' } });

    fireEvent.change(screen.getByLabelText(/recruiter/i), { target: { value: '1' } });
    fireEvent.change(screen.getByLabelText(/department/i), { target: { value: '2' } });

    fireEvent.click(screen.getByText(/create/i));

    expect(await screen.findByRole('dialog')).toBeInTheDocument();

    fireEvent.click(screen.getByText('Create'));

    await waitFor(() => {
      expect(mockOnSubmit).toHaveBeenCalledTimes(1);
    });
  });
});