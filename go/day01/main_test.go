package main

import (
	"testing"
)

// func TestMain(*testing.T) {
// 	main()
// }

func TestPart1(t *testing.T) {
	input := `3   4
4   3
2   5
1   3
3   9
3   3`

	expected := 11
	got := part1(input)

	if got != expected {
		t.Errorf("Got = %v; want %v", got, expected)
	}
}
func TestPart2(t *testing.T) {
	input := `3   4
4   3
2   5
1   3
3   9
3   3`

	want := 0
	got := part2(input)

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}
func TestExample(t *testing.T) {
	tests := []struct {
		name     string
		input    int
		expected int
	}{
		{name: "case 1", input: 1, expected: 1},
		{name: "case 2", input: -4, expected: 4},
		{name: "case 3", input: -3, expected: 3},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			got := absInt(tt.input)
			if got != tt.expected {
				t.Errorf("got %v, want %v", got, tt.expected)
			}
		})
	}
}
