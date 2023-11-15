import React, { useEffect } from "react";
import { useCallback, useRef, useState, useImperativeHandle } from "react";

interface IProps {
  title: string;
  buttonText: string;
  onClickAction: () => void;
  children: React.ReactNode;
}

export interface IActions {
  show: () => void;
  hide: () => void;
  visibilityRender: () => void;
}

//So far this is essentially an exact clone of delete-modal.tsx, keeping them separate for now until I am sure there is no difference.
const AddEditModal: React.ForwardRefExoticComponent<
  IProps & React.RefAttributes<IActions>
> = React.forwardRef<IActions, IProps>(
  ({ title, buttonText, onClickAction, children }, ref): JSX.Element => {
    const refDialog = useRef<HTMLDialogElement>(null);
    const [visibility, setVisibility] = useState<boolean>(false);

    useImperativeHandle(ref, () => ({
      show,
      hide,
      visibilityRender,
    }));

    const show = useCallback((): void => {
      setVisibility(true);
    }, []);

    const hide = useCallback((): void => {
      setVisibility(false);
    }, []);

    const visibilityRender = useCallback((): void => {
      var m = refDialog?.current;

      if (!m) return;

      if (m.open && !visibility) {
        m.close();

        setVisibility(false);
      } else if (!m.open && visibility) {
        m.showModal();
      }
    }, [refDialog, visibility]);

    useEffect(() => {
      visibilityRender();
    }, [visibility, visibilityRender]);

    return (
      <dialog id="addEditModal" ref={refDialog}>
        <article>
          <header>
            <a
              href="#"
              aria-label="Close"
              className="close"
              onClick={() => setVisibility(false)}
            ></a>
            {title}
          </header>
          <div>{children}</div>
          <footer>
            <button
              className="contrast outline"
              onClick={() => {
                onClickAction();
                setVisibility(false);
              }}
            >
              {buttonText}
            </button>
          </footer>
        </article>
      </dialog>
    );
  }
);

AddEditModal.displayName = "AddEditModal";

export default AddEditModal;
