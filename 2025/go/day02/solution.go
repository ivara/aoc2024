package main

import (
	"fmt"
	"strconv"
	"strings"
	"sync"
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
		// fmt.Printf("Current sequence %v\n", sequence)
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

func part2v2(input string) int {
	sum := 0
	ranges := strings.SplitSeq(input, ",")
	for sequence := range ranges {
		// fmt.Printf("Current sequence %v\n", sequence)
		parts := strings.Split(sequence, "-")
		start, _ := strconv.Atoi(parts[0])
		stop, _ := strconv.Atoi(parts[1])

		for current := start; current <= stop; current++ {
			if isRepeating(strconv.Itoa(current)) {
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
	stop := len(stringValue) / 2
	for chunkSize := 1; chunkSize <= stop; chunkSize++ {
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

// Another way of checking (used in part2v2)
func isRepeating(s string) bool {
	for size := 1; size <= len(s)/2; size++ {
		if len(s)%size == 0 && strings.Repeat(s[:size], len(s)/size) == s {
			return true
		}
	}
	return false
}

func part2v3(input string) int {
	var wg sync.WaitGroup
	results := make(chan int)

	// Convert iterator to slice so we can spawn goroutines
	ranges := strings.SplitSeq(input, ",")
	sequences := make([]string, 0)
	for sequence := range ranges {
		sequences = append(sequences, sequence)
	}

	// Process each sequence in a separate goroutine
	for _, sequence := range sequences {
		wg.Add(1)
		go func(seq string) {
			defer wg.Done()
			partialSum := 0

			parts := strings.Split(seq, "-")
			start, _ := strconv.Atoi(parts[0])
			stop, _ := strconv.Atoi(parts[1])

			for current := start; current <= stop; current++ {
				if isInvalidId(current) {
					partialSum += current
				}
			}

			results <- partialSum
		}(sequence)
	}

	// Close results channel when all goroutines complete
	go func() {
		wg.Wait()
		close(results)
	}()

	// Sum up all partial results
	sum := 0
	for partialSum := range results {
		sum += partialSum
	}

	return sum
}
