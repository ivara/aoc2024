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
		move, _ := strconv.Atoi(line[1:])

		if direction == "L" {
			move = -move
		}

		dialValue = (dialValue + move) % 100
		if dialValue < 0 {
			dialValue += 100
		}

		fmt.Printf("The dial is rotated %v to point at %v\n", line, dialValue)
		if dialValue == 0 {
			sum += 1
		}
	}
	return sum
}

func part2(input string) int {
	currentDialValue := 50
	sum := 0

	lines := strings.SplitSeq(input, "\n")
	for line := range lines {
		direction := line[:1]
		move, _ := strconv.Atoi(line[1:])
		if direction == "L" {
			move = -move
		}

		newDialValue, zeroTicks := turnDial(currentDialValue, move)
		currentDialValue = newDialValue

		if zeroTicks > 0 {
			sum += zeroTicks
		}

		// fmt.Printf("The dial is rotated %v to point at %v\n", line, newDialValue)
	}
	return sum
}

// Returns how many times we landed on Zero
func turnDial(start int, move int) (newDialValue, zeroTicks int) {
	wheelSize := 100
	zeroTicks = Abs(move / wheelSize)
	remainder := move % wheelSize
	newDialValue = (((start + move) % wheelSize) + wheelSize) % 100

	// Different logic for clockwise vs counter clockwise
	switch move < 0 {
	case true:
		{
			// the remainder can only cause "pointing" on 0
			// if we didn't start on 0
			// Looks a bit wonky with "start+remainder"
			// but remainder is a negative number
			if start > 0 && (start+remainder <= 0) {
				zeroTicks += 1
			}
			break
		}
	case false:
		{
			if start+remainder > 99 {
				zeroTicks += 1
			}
			break
		}
	}

	// fmt.Printf("Laps %v and rest %v", laps, rest)
	fmt.Printf("Start %v, move %v, zeroTicks = %v\n", start, move, zeroTicks)
	return newDialValue, zeroTicks
}

func Abs(x int) int {
	if x < 0 {
		return -x
	}
	return x
}
