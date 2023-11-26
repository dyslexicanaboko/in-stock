interface IProps {
  children: React.ReactNode;
}

//Absolute nightmare to pass components to your custom component
//https://linguinecode.com/post/pass-react-component-as-prop-with-typescript
const TabContainer: React.FC<IProps> = (props: IProps) => {
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

export default TabContainer;
