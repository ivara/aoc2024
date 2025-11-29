package main

import (
	"testing"
)

func TestPart1(t *testing.T) {
	input := `1000
2000
3000

4000

5000
6000

7000
8000
9000

10000
`

	got := part1(input)
	want := 24000

	if got != want {
		t.Errorf("Got = %v; want %v", got, want)
	}
}
