export const getUserId = () : number => {
  const userId = localStorage.getItem("user-id");

  if(!userId) {
    //Log out?
    return 0;
  }

  return parseInt(userId);
}
