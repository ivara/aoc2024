package main

import (
	"testing"
)

// func TestGetScore(t *testing.T) {
// 	tests := []struct {
// 		name     string
// 		line     string
// 		expected int
// 	}{
// 		// Valid ones
// 		{"Only ones, length 3 size 1", "A Y", 8},
// 	}

//		for _, tt := range tests {
//			t.Run(tt.name, func(t *testing.T) {
//				got := getScore(tt.line)
//				if got != tt.expected {
//					t.Errorf("getScore(%v) = %v; want %v", tt.line, got, tt.expected)
//				}
//			})
//		}
//	}
func TestPart1(t *testing.T) {
	input := `A Y
B X
C Z`

	got := part1(input)
	want := 15

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

func TestPart2(t *testing.T) {
	input := `A Y
B X
C Z`

	got := part2(input)
	want := 12

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}
