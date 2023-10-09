import {
  PositionV1CreateModel,
  PositionV1PatchModel,
} from "../../services/in-stock-api-models";

export type PositionModal = {
  title: string;
  action: (position: PositionModel) => void;
};

export type PositionModel = {
  stockId: number;
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
  price: number;
  quantity: number;
};

//Maybe I can try this later... don't like that I am using another model as an intermediary
export interface IPositionModel {
  stockId: number;
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
  price: number;
  quantity: number;
}

export enum Mode {
  Add,
  Edit,
}
