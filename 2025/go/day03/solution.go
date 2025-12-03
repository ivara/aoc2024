package main

import (
	"fmt"
	"slices"
	"strconv"
	"strings"
)

func part1(input string) int {
	sum := 0
	lines := strings.SplitSeq(input, "\n")
	for line := range lines {
		length := len(line)
		ogArr := make([]byte, length)

		// copy
		for i, r := range line {
			ogArr[i] = byte(r)
		}

		sortedOgArr := make([]byte, length-1)
		copy(sortedOgArr, ogArr[:len(ogArr)-1])
		// firstCopy := sortedOgArr[:length-1]
		slices.Sort(sortedOgArr)
		firstMaxValue := sortedOgArr[len(sortedOgArr)-1]

		// calculate 2nd
		maxValueIndex := slices.Index(ogArr, firstMaxValue)
		sortedSecondArr := make([]byte, len(ogArr[maxValueIndex+1:]))
		copy(sortedSecondArr, ogArr[maxValueIndex+1:])
		slices.Sort(sortedSecondArr)
		secondMaxValue := sortedSecondArr[len(sortedSecondArr)-1]

		fmt.Printf("Joltage for %v: %v%v\n", line, string(firstMaxValue), string(secondMaxValue))
		joltage := string(firstMaxValue) + string(secondMaxValue)
		jolt, _ := strconv.Atoi(joltage)
		sum += jolt
	}

	return sum
}

// Turn on 12 batteries
func part2(input string) int {
	var sum int = 0

	lines := strings.SplitSeq(input, "\n")
	for line := range lines {
		// Process one line at a time
		sum += getJoltage(line, 12)
	}

	return sum
}

func getJoltage(s string, size int) int {
	fullLength := len(s)
	ogArr := make([]byte, len(s))
	windowStart := 0

	joltages := make([]byte, size)

	// copy
	for i, r := range s {
		ogArr[i] = byte(r)
	}

	// make temporary variable to hold our "current window" to search in
	// it is never larger than from pos 0 to length-size
	// window := make([]byte, (fullLength-size)+1)

	// Set joltages
	for i := 1; i <= size; i++ {
		// find greatest number in string, except (size-i) last positions
		// e.g. finding the first out of twelve, we need 11 more
		windowStop := fullLength - (size - i)
		slidingWindow := make([]byte, windowStop-windowStart)
		copy(slidingWindow, s[windowStart:windowStop])

		// sort window and find largest number
		slices.Sort(slidingWindow)
		joltage := slidingWindow[len(slidingWindow)-1]
		joltages[i-1] = joltage
		windowStart = slices.Index(ogArr[windowStart:windowStop], joltage) + 1 + windowStart
	}

	// Build
	yay, _ := strconv.Atoi(string(joltages))
	return yay
}
