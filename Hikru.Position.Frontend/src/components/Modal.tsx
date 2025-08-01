import React, { useEffect, useRef } from 'react';

interface ModalProps {
  title: string;
  children: React.ReactNode;
  onClose: () => void;
  onConfirm: () => void;
  confirmText?: string;
  cancelText?: string;
}

const Modal: React.FC<ModalProps> = ({
  title,
  children,
  onClose,
  onConfirm,
  confirmText = "Confirm",
  cancelText = "Cancel"
}) => {
  const confirmButtonRef = useRef<HTMLButtonElement>(null);
  const modalTitleId = 'modal-title';
  const modalDescId = 'modal-description';

  useEffect(() => {
    confirmButtonRef.current?.focus();
  }, []);
  
  return (
    <div className="modal-overlay">
      <div className="modal-box"
        role="dialog"
        aria-modal="true"
        aria-labelledby={modalTitleId}
        aria-describedby={modalDescId}>
        <h2 className="modal-title" id={modalTitleId}>{title}</h2>
        <div className="modal-content" id={modalDescId}>
          {children}
        </div>
        <div className="modal-actions">
          <button className="modal-cancel" onClick={onClose}>
            {cancelText}
          </button>
          <button
            className="modal-confirm"
            onClick={onConfirm}
            ref={confirmButtonRef}>
            {confirmText}
          </button>
        </div>
      </div>
    </div>
  );
};

export default Modal;