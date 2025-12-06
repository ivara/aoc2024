const fs = require("fs");

const contents = fs
  .readFileSync("./input.txt", "utf8")
  .split("\n")
  .map((line) => line.split(""));

//console.log(contents);

const dirs = [
  [-1, -1],
  [0, -1],
  [1, -1],
  [-1, 0],
  [1, 0],
  [-1, 1],
  [0, 1],
  [1, 1],
];

/** @param {string} char **/
function isCharNumber(char) {
  return !isNaN(parseInt(char));
}

/** @returns character from i,j */
function get(y, x, [dy, dx]) {
  console.log("y, x, [dy, dx]", y, x, [dy, dx]);

  const chars = contents[y + dy];

  // check if row exists
  if (chars === undefined) {
    return undefined;
  }

  return chars[x + dx];
}

function isDot(char) {
  return char === ".";
}

let sum = 0;
// loop rows
for (let y = 0; y < contents.length; y++) {
  //console.log('row row row');
  const row = contents[y];

  let isNumber = false;
  let currentNumber = "";
  let check = true;

  // loop each char in a row
  for (let x = 0; x < row.length; ++x) {
    isNumber = isCharNumber(get(y, x, [0, 0]));

    if (!isNumber && !check) {
      console.log("currentNumber", currentNumber);
      sum += parseInt(currentNumber);
    }

    // reset
    if (!isNumber) {
      currentNumber = "";
      check = true;
    }

    if (isNumber && check) {
      // check all directions around this number
      // determine if
      const is = dirs.reduce((acc, [dy, dx]) => {
        //console.log('acc', acc);
        const char = get(y, x, dy, dx);
        return (
          acc || (!isDot(char) && !isCharNumber(char) && char !== undefined)
        );
      }, false);

      if (is) {
        check = false;
      }
    }

    console.log("currentNumber", currentNumber);
    if (isNumber) {
      currentNumber += get(y, x, [0, 0]);
    }
  }
}
