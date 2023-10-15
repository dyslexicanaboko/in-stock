import React, { useEffect } from "react";
import { useCallback, useRef, useState, useImperativeHandle } from "react";

interface IProps {
  title: string;
  onClickAction: () => void;
  children: React.ReactNode;
}

export interface IActions {
  show: () => void;
  hide: () => void;
  visibilityRender: () => void;
}

//https://linguinecode.com/post/pass-react-component-as-prop-with-typescript
//Notice: Not using React.FC<T> here because it wouldn't support the `useImpreativeHandle` hook's need to expose the ref.
const DeleteModal: React.ForwardRefExoticComponent<
  IProps & React.RefAttributes<IActions>
> = React.forwardRef<IActions, IProps>(
  ({ title, onClickAction, children }, ref): JSX.Element => {
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
      <dialog id="deleteModal" ref={refDialog}>
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
              Delete
            </button>
          </footer>
        </article>
      </dialog>
    );
  }
);

DeleteModal.displayName = "DeleteModal";

export default DeleteModal;
