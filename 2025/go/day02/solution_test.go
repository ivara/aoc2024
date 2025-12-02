package main

import "testing"

func TestIsValidId(t *testing.T) {
	tests := []struct {
		name      string
		whole     string
		chunkSize int
		expected  bool
	}{
		// Valid ones
		{"Only ones, length 3 size 1", "111", 1, true},
		{"Invalid due to length", "1188511885", 5, true},
		// Invalid ones
		{"Only ones, length 3 size 2", "111", 2, false},
		{"Incompatible length and size", "12121", 2, false},
		{"Invalid due to mismatch", "1234", 2, false},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			got := hasRepeatingChunk(tt.whole, tt.chunkSize)
			if got != tt.expected {
				t.Errorf("isRepeated(%v, %v) = %v; want %v", tt.whole, tt.chunkSize, got, tt.expected)
			}
		})
	}
}

func TestPart1(t *testing.T) {
	input := `11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124`
	got := part1(input)
	want := 1227775554

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}

// part1 correct answer: 24157613387
