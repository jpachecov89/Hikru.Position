import { render, screen, fireEvent } from '@testing-library/react';
import Modal from '../../components/Modal';

describe('Modal component', () => {
  const title = 'Confirm Action';
  const confirmText = 'Confirm';
  const cancelText = 'Cancel';
  const children = <p>This action cannot be undone.</p>;

  test('renders title and content correctly', () => {
    render(
      <Modal
        title={title}
        onConfirm={jest.fn()}
        onClose={jest.fn()}
        confirmText={confirmText}
        cancelText={cancelText}
      >
        {children}
      </Modal>
    );

    expect(screen.getByRole('dialog')).toBeInTheDocument();
    expect(screen.getByText(title)).toBeInTheDocument();
    expect(screen.getByText('This action cannot be undone.')).toBeInTheDocument();
  });

  test('calls onConfirm when confirm button is clicked', () => {
    const onConfirm = jest.fn();

    render(
      <Modal
        title={title}
        onConfirm={onConfirm}
        onClose={jest.fn()}
        confirmText={confirmText}
        cancelText={cancelText}
      >
        {children}
      </Modal>
    );

    fireEvent.click(screen.getByText(confirmText));
    expect(onConfirm).toHaveBeenCalledTimes(1);
  });

  test('calls onClose when cancel button is clicked', () => {
    const onClose = jest.fn();

    render(
      <Modal
        title={title}
        onConfirm={jest.fn()}
        onClose={onClose}
        confirmText={confirmText}
        cancelText={cancelText}
      >
        {children}
      </Modal>
    );

    fireEvent.click(screen.getByText(cancelText));
    expect(onClose).toHaveBeenCalledTimes(1);
  });

  test('focus is set on confirm button when modal mounts', () => {
    render(
      <Modal
        title={title}
        onConfirm={jest.fn()}
        onClose={jest.fn()}
        confirmText={confirmText}
        cancelText={cancelText}
      >
        {children}
      </Modal>
    );

    const confirmButton = screen.getByText(confirmText);
    expect(confirmButton).toHaveFocus();
  });
});