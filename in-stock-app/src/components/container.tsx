interface IParentComponent {
  children: React.ReactNode;
}

//Absolute nightmare to pass components to your custom component
//https://linguinecode.com/post/pass-react-component-as-prop-with-typescript
const Container : React.FC<IParentComponent> = (props: IParentComponent) => {
  const { children } = props;

  return (
    <main className="container">
      <div className="grid">
        <div />
        {children}
        <div />
      </div>
    </main>
  );
};

export default Container;
