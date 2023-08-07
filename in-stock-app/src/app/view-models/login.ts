export type UserV1PostModel = {
  username: string;
  password: string;
};

export type LoginResult = {
  isSuccess: boolean;
  error?: string;
};

export type RefreshTokenV1PostModel = {
  refreshToken: string;
};
