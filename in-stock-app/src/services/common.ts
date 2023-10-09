export const EmptyString: string = "";

export const DaysInOneYear: number = 365.0;

export const redirectToLoginPage = (): void => {
  window.location.href = "/";
};

function assertIsNull(value: any): asserts value {
  if (value === null) throw new Error("Assert failed");
}

export const isNull = (value: any): boolean => {
  try {
    assertIsNull(value);

    return true;
  } catch (error) {
    return false;
  }
};

export const isNotNull = (value: any): boolean => value !== null;

const n2 = (value: string): string => {
  if (value.includes(".")) return value;

  return value + ".00";
};

export const toFloat = (value: string): number => parseFloat(n2(value));

let _offsetMinutes: number | undefined = undefined;

export const localOffsetMinutes = (): number => {
  if (_offsetMinutes !== undefined) return _offsetMinutes;

  _offsetMinutes = new Date().getTimezoneOffset();

  return _offsetMinutes;
};

/**
 * Because HTML input-date is stupid and doesn't take into account the local time zone
 * here is a function to add the offset when working with the date component only.
 * @param dateComponentOnly existing date that is missing the time component.
 * @returns new Date with offset applied.
 */
export const getDateWithOffset = (dateComponentOnly: Date): Date => {
  const date = new Date(dateComponentOnly);

  date.setMinutes(date.getMinutes() + localOffsetMinutes());

  return date;
};
