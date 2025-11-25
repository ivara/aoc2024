package main

import (
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	// input := readFileContents("test.txt")
	input := readFileContents("input.txt")
	result := part1(input)

	fmt.Println("Result: ", result)
}

func part1(input string) int {
	lines := strings.Split(input, "\n")
	sum := 0
	for _, line := range lines {
		numbers, _ := stringsToInts(strings.Split(line, " "))
		if isSafe(numbers) {
			sum += 1
		}
	}
	return sum
}

func part2(input string) int {
	lines := strings.Split(input, "\n")
	sum := 0
	for _, line := range lines {
		numbers, _ := stringsToInts(strings.Split(line, " "))

		if isSafe(numbers) {
			sum += 1
		} else {
			// brute force trial-and-error
			for i := range numbers {
				// Try removin n:th entry in numbers array and see if it becomes a safe line
				// remove 2nd
				// johan := append(numbers)
				newNumbers := make([]int, len(numbers))
				copy(newNumbers, numbers)
				newNumbers = append(newNumbers[:i], newNumbers[i+1:]...)
				if isSafe(newNumbers) {
					sum += 1
					break
				}
			}
		}

	}
	return sum
}

func stringsToInts(strings []string) ([]int, error) {
	ints := make([]int, len(strings))
	for i, s := range strings {
		num, err := strconv.Atoi(s)
		if err != nil {
			return nil, err
		}
		ints[i] = num
	}
	return ints, nil
}

func absInt(x int) int {
	if x < 0 {
		return -x
	}
	return x
}

func isSafe(numbers []int) bool {
	up := numbers[0] < numbers[1]
	size := len(numbers)

	for i := range numbers {
		// last element? Then we're done!
		if i == size-1 {
			return true
		}
		// check 1
		iUp := numbers[i] < numbers[i+1]

		if up != iUp {
			return false
		}

		// check 2
		diff := absInt(numbers[i] - numbers[i+1])
		if diff < 1 || diff > 3 {
			return false
		}
	}

	return true
}

func readFileContents(filePath string) string {
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal("Error reading file:", err)
	}

	return string(data)
}
