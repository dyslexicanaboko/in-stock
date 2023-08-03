//https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Intl
//https://stackoverflow.com/questions/48621533/how-convert-date-format-in-javascript-reactjs
//https://stackoverflow.com/questions/44784774/in-react-how-to-format-a-number-with-commas

const dateTimeFormat = new Intl.DateTimeFormat("en-US", {
  year: "numeric",
  month: "2-digit",
  day: "2-digit",
  hour: "2-digit",
  minute: "2-digit",
  second: "2-digit",
});

const dateOnlyFormat = new Intl.DateTimeFormat("en-US", {
  year: "2-digit",
  month: "2-digit",
  day: "2-digit",
});

const currencyFormat = new Intl.NumberFormat("en-US", {
  style: "currency",
  currency: "USD",
  currencySign: "accounting",
  minimumFractionDigits: 2,
});

const percentFormat = new Intl.NumberFormat("en-US", {
  style: "percent",
  minimumFractionDigits: 2,
});

export const formatDate = (date: Date): string =>
  dateOnlyFormat.format(new Date(date));

export const formatDateTime = (dateTime: Date): string =>
  dateTimeFormat.format(new Date(dateTime));

export const formatNumber = (number: number, places: number = 0): string => {
  const nf = new Intl.NumberFormat("en-US", {
    minimumFractionDigits: places,
  });

  return nf.format(number);
};

export const formatCurrency = (amount: number): string =>
  currencyFormat.format(amount);

export const formatPercent = (ratio: number): string =>
  percentFormat.format(ratio);
