import run from "aocrunner";
import { assert } from "node:console";
import test, { describe } from "node:test";

const parseInput = (rawInput: string) => rawInput;

const part1 = (rawInput: string) => {
  const input = parseInput(rawInput);
  var lines = input.split('\n');

  let sum = 0;
  lines.forEach(line => {
    let tmp = parseLine(line);
    sum += tmp;
  });

  return sum;
};

function parseLine(line: string): number {
  // Remove everything except numbers
  const numbersOnly = line.replace(/\D/g, "");
  //console.log('numbersOnly: ' + numbersOnly);
  if(numbersOnly.length == 0)
    return 0;

  if(numbersOnly.length == 1)
    return parseInt(numbersOnly+numbersOnly);
 
  // add first and last number
  return parseInt(numbersOnly[0] + numbersOnly[numbersOnly.length - 1]);
}


const part2 = (rawInput: string) => {
  const input = parseInput(rawInput);
  var lines = input.split('\n');

  let sum = 0;
  lines.forEach(line => {
    // part2 update
    // start by replacing 1-9 numbers as text with numbers
    //line = replaceWordsWithNumbers(line);
    let left = findLeft(line);
    let right = findRight(line);
    // console.log('line: ' + line);
    // console.log('left: ' + left + ' right: ' + right);
    // console.log('sum to add: ' + parseInt(left+right));
    // console.log('-------------------');
    //let tmp = parseLine(line);

    sum += parseInt(left+right) ?? 0;
  });

  return sum;
};

function findLeft(line: string): string {

  var withoutwords = replaceWordsWithNumbers(line);

  const regex = /\d+/g;
  let matches = regex.exec(withoutwords);
  if(matches == null)
    return "";

    var match = matches.shift() ?? "";
    var firstDigit = match[0] ?? 0;
    return firstDigit;
}

function findRight(line: string): string {
  //console.log('findRight original line: ' + line);

  let reversedLine = line.split("").reverse().join("");
  let withoutwords = replaceWordsWithNumbersBackwards(reversedLine);
  //console.log('findRight withoutwords: ' + withoutwords);

  return findLeft(withoutwords);
  // const regex = /\d+/g;
  // let matches = regex.exec(withoutwords);
  // if (matches == null) {
  //   return "";
  // }
  // let lastMatch = matches[matches.length - 1];
  // console.log('lastmatch: ' + lastMatch);
  // let lastDigit = lastMatch[lastMatch.length - 1];
  // return lastDigit;
}

function replaceWordsWithNumbers(input : string): string {
  const replacements : Record<string, string> = {
    "one": "1",
    "two": "2",
    "three": "3",
    "four": "4",
    "five": "5",
    "six": "6",
    "seven": "7",
    "eight": "8",
    "nine": "9"
  };

  const regex = new RegExp(Object.keys(replacements).join("|"), "gi");

  return input.replace(regex, match => replacements[match.toLowerCase()]);
}

function replaceWordsWithNumbersBackwards(input : string): string {
  const replacements : Record<string, string> = {
    "eno": "1",
    "owt": "2",
    "eerht": "3",
    "ruof": "4",
    "evif": "5",
    "xis": "6",
    "neves": "7",
    "thgie": "8",
    "enin": "9"
  };
  const regex = new RegExp(Object.keys(replacements).join("|"), "gi");
  return input.replace(regex, match => replacements[match.toLowerCase()]);
}




run({
  part1: {
    tests: [
      {
        input: `
        1abc2
        pqr3stu8vwx
        a1b2c3d4e5f
        treb7uchet`,
        expected: 142,
      },
      {
        input: `1a`,
        expected: 11,
      },
      {
        input: `2a`,
        expected: 22,
      },
      {
        input: `1a2`,
        expected: 12,
      },
      {
        input: `1a2b3c`,
        expected: 13,
      },
      {
        input: ``,
        expected: 0,
      },

    ],
    solution: part1,
  },
  part2: {
    tests: [
      {
        input: `
          two1nine
          eightwothree
          abcone2threexyz
          xtwone3four
          4nineeightseven2
          zoneight234
          7pqrstsixteen`,
        expected: 281,
      },
      {
        input: `onetwothree`,
        expected: 13,
      },
      {
        input: `cqmzqbxzfvonevmmmlxsnjr5zfg`,
        expected: 15,
      },
      {
        input: `onefive29htsdkllvr`,
        expected: 19,
      },
      {
        input: `OnEfivE29htsdkllvrSeVEN`,
        expected: 17,
      },
      {
        input: `92`,
        expected: 92,
      },
      {
        input: `9five1`,
        expected: 91,
      },
      {
        input: `eightwo`,
        expected: 82,
      }
    ],
    solution: part2,
  },
  trimTestInputs: true,
  onlyTests: false,
});
