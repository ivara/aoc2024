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
	rest := move % wheelSize
	newDialValue = (((start + move) % wheelSize) + wheelSize) % 100

	switch rest < 0 {
	case true:
		{
			// moving counter clock-wise (negative move)
			// if newDialValue < 0 {
			// 	newDialValue += 100 // if we have "-18" this becomes +82
			// }

			// did we tick 0 again?
			if start > 0 && (start+rest <= 0) {
				zeroTicks += 1
			}
			break
		}
	case false:
		{
			// moving clock-wise (positive move)
			// newDialValue = (start + move) % 100
			if start != 0 && start+rest > 99 {
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

// Imagine a wheel consisting of 0 - N numbers
// once you go beyond N, you are back on 0 and continue from there
// This method tells you how many times you passed 0
// excluding if it landed on a zero
func ZeroCrossings(start, move int) int {
	N := 100

	if move == 0 || (move > 0 && (start+move) <= N) {
		return 0
	}

	if move < 0 && (start+move >= 0) {
		return 0
	}

	if start == 0 && Abs(move) <= N {
		return 0
	}

	// how many full laps? (move 595 for example, is 5 guaranteeed, and 95 might be one more)
	// and on final lap, did we cross 0 ?

	// How many times do we pass 0?????
	// diff is -1, crossed 0 once
	laps := Abs(move) / N

	rest := move % N

	johan := start + rest
	if johan < 0 || johan > 99 {
		return laps + 1
	} else {
		return laps
	}

	// if move < 0 && rest > start {
	// 	return laps + 1
	// } else if move > 0 && (N-start) < move {
	// 	return laps + 1
	// } else {
	// 	return laps
	// }
}
