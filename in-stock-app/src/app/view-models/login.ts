export type Login = {
  username: string;
  password: string;
};

export type LoginResult = {
  isSuccess: boolean;
  error?: string;
};
