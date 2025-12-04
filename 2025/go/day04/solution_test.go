package main

import (
	"log"
	"os"
	"testing"
)

// func TestIsValidId(t *testing.T) {
// 	tests := []struct {
// 		name      string
// 		whole     string
// 		chunkSize int
// 		expected  bool
// 	}{
// 		// Valid ones
// 		{"Only ones, length 3 size 1", "111", 1, true},
// 		{"Invalid due to length", "1188511885", 5, true},
// 		// Invalid ones
// 		{"Only ones, length 3 size 2", "111", 2, false},
// 		{"Incompatible length and size", "12121", 2, false},
// 		{"Invalid due to mismatch", "1234", 2, false},
// 	}

// 	for _, tt := range tests {
// 		t.Run(tt.name, func(t *testing.T) {
// 			got := hasRepeatingChunk(tt.whole, tt.chunkSize)
// 			if got != tt.expected {
// 				t.Errorf("hasRepeatingChunk(%v, %v) = %v; want %v", tt.whole, tt.chunkSize, got, tt.expected)
// 			}
// 		})
// 	}
// }

// func TestIsRepeating(t *testing.T) {
// 	tests := []struct {
// 		name     string
// 		str      string
// 		expected bool
// 	}{
// 		// Valid ones
// 		{"Is repeating only ones", "111", true},
// 	}

// 	for _, tt := range tests {
// 		t.Run(tt.name, func(t *testing.T) {
// 			got := isRepeating(tt.str)
// 			if got != tt.expected {
// 				t.Errorf("isRepeating(%v) = %v; want %v", tt.str, got, tt.expected)
// 			}
// 		})
// 	}
// }

func TestPart1(t *testing.T) {
	input := []byte(`..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.`)
	got := part1(input)
	want := 13

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

func TestPart2(t *testing.T) {
	input := []byte(`..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.`)
	got := part2(input)
	want := 43

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

// func TestPart2(t *testing.T) {
// 	input := `11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124`
// 	got := part2(input)
// 	want := 4174379265

// 	if got != want {
// 		t.Errorf("Got = %v; want %v", got, want)
// 	}
// }

// func TestPart2v2(t *testing.T) {
// 	input := `11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124`
// 	got := part2v2(input)
// 	want := 4174379265

// 	if got != want {
// 		t.Errorf("Got = %v; want %v", got, want)
// 	}
// }

// // part1 correct answer: 24157613387

// ---------------------------------------------------
//
//	BENCHMARKS
//
// ---------------------------------------------------
func BenchmarkPart2TestData(b *testing.B) {
	input := []byte(`..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.`)

	for b.Loop() {
		part2(input)
	}
}
func BenchmarkPart2RealData(b *testing.B) {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		log.Fatal("Error reading file:", err)
	}

	for b.Loop() {
		part2(data)
	}
}
