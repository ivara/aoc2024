package main

import (
	"fmt"
	"strconv"
	"strings"
)

func part1(input string) int {
	sum := 0
	ranges := strings.SplitSeq(input, ",")
	for sequence := range ranges {
		fmt.Printf("Current sequence %v\n", sequence)
		parts := strings.Split(sequence, "-")
		start, _ := strconv.Atoi(parts[0])
		stop, _ := strconv.Atoi(parts[1])

		for current := start; current <= stop; current++ {
			strValue := strconv.Itoa(current)
			strLength := len(strValue)
			if strLength%2 != 0 {
				continue
			}
			firstHalf := strValue[:strLength/2]
			lastHalf := strValue[strLength/2:]
			if firstHalf == lastHalf {
				sum += current
			}
		}
	}

	return sum
}

func part2(input string) int {
	sum := 0
	ranges := strings.SplitSeq(input, ",")
	for sequence := range ranges {
		fmt.Printf("Current sequence %v\n", sequence)
		parts := strings.Split(sequence, "-")
		start, _ := strconv.Atoi(parts[0])
		stop, _ := strconv.Atoi(parts[1])

		for current := start; current <= stop; current++ {
			if isInvalidId(current) {
				sum += current
			}
		}
	}

	return sum
}

// loop over the id incrementally as a string
// and check if it has a repeated sequence
func isInvalidId(id int) bool {
	stringValue := strconv.Itoa(id)
	stop := len(stringValue)
	for chunkSize := 1; chunkSize <= stop/2; chunkSize++ {
		if hasRepeatingChunk(stringValue, chunkSize) {
			return true
		}
	}
	return false
}

func hasRepeatingChunk(s string, chunkSize int) bool {
	if len(s)%chunkSize != 0 {
		return false
	}

	pattern := s[:chunkSize]
	for i := chunkSize; i < len(s); i += chunkSize {
		if s[i:i+chunkSize] != pattern {
			return false
		}
	}
	return true
}
