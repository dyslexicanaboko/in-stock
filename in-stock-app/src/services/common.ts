export const EmptyString : string = "";

export const DaysInOneYear : number = 365.0;

export const redirectToLoginPage = () : void => {
  window.location.href = "/";
}

function assertIsNull(value: any) : asserts value  {
  if(value === null) throw new Error("Assert failed");
} 

export const isNull = (value: any) : boolean => {
  try {
    assertIsNull(value);

    return true;
  } catch (error) {
    return false;
  }
}

export const isNotNull = (value: any) : boolean => value !== null;
