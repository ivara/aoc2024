package main

import (
	"fmt"
	"strconv"
	"strings"
)

func part1(input string) int {
	dialValue := 50
	sum := 0

	lines := strings.SplitSeq(input, "\n")
	for line := range lines {
		direction := line[:1]
		amount, err := strconv.Atoi(line[1:])
		if err != nil {
			return 0
			// fmt.Printf("Error converting string to int:", err)
		}

		fmt.Printf("Direction: %s, Amount: %d\n", direction, amount)
		if direction == "L" {
			dialValue -= amount
		} else {
			dialValue += amount
		}

		fmt.Printf("New dial value: %d\n", dialValue)
		if dialValue == 0 {
			sum += 1
		}
	}
	return sum
}

func part2(input string) int {
	return 0
}
