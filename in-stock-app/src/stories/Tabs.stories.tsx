import type { Meta, StoryObj } from "@storybook/react";
import "../app/globals.css";

import TabContainer from "../components/tab-container";

// More on how to set up stories at: https://storybook.js.org/docs/react/writing-stories/introduction#default-export
const meta = {
  title: "Components/Tab Container",
  component: TabContainer,
  parameters: {
    // Optional parameter to center the component in the Canvas. More info: https://storybook.js.org/docs/react/configure/story-layout
    layout: "centered",
  },
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/react/writing-docs/autodocs
  tags: ["autodocs"],
  // More on argTypes: https://storybook.js.org/docs/react/api/argtypes
  argTypes: {},
} satisfies Meta<typeof TabContainer>;

export default meta;
type Story = StoryObj<typeof meta>;

const content = (index: number): JSX.Element => {
  return (
    <>
      <hr />
      <div>
        <span>tab {index}</span>
      </div>
    </>
  );
};

// More on writing stories with args: https://storybook.js.org/docs/react/writing-stories/args
export const OneTab: Story = {
  args: {
    tabs: [
      { title: "Tab 1", selected: false, content: content(1) },
      { title: "Tab 2", selected: false, content: content(2) },
      { title: "Tab 3", selected: false, content: content(3) },
    ],
  },
};
