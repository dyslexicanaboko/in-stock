import { useRef, useState } from "react";

interface IProps {
  tabs: ITab[];
}

interface ITab {
  title: string;
  selected: boolean;
  content: React.ReactNode;
}

const TabContainer: React.FC<IProps> = (props: IProps) => {
  const tabs = useRef<ITab[]>(props.tabs);
  const [currentIndex, setCurrentIndex] = useState<number>(0);

  let selectedIndex = tabs.current.findIndex((tab) => tab.selected);

  if (selectedIndex === -1) selectedIndex = 0;

  const changeTab = (index: number): void => {
    tabs.current.forEach((tab, i) => {
      tab.selected = i === index;
    });

    setCurrentIndex(index);
  };

  return (
    <article>
      <header>
        <nav>
          <ul>
            {tabs.current.map((tab, i) => {
              const role = i === selectedIndex ? "button" : "";

              return (
                <li key={i}>
                  <button role={role} onClick={() => changeTab(i)}>
                    {tab.title}
                  </button>
                </li>
              );
            })}
          </ul>
        </nav>
      </header>
      {tabs.current[selectedIndex].content}
    </article>
  );
};

export default TabContainer;
