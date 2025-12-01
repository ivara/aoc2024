package main

import (
	"container/heap"
	"strconv"
	"strings"
)

func part1(input string) int {
	max := 0
	chunks := strings.SplitSeq(input, "\n\n")
	for chunk := range chunks {
		sum := 0
		calories := strings.SplitSeq(chunk, "\n")
		for calorie := range calories {
			calInt, _ := strconv.Atoi(calorie)
			sum += calInt
		}
		if sum > max {
			max = sum
		}
	}
	return max
}

func part2(input string) int {
	h := &MinHeap{}
	heap.Init(h)

	chunks := strings.SplitSeq(input, "\n\n")
	for chunk := range chunks {
		sum := 0
		calories := strings.SplitSeq(chunk, "\n")
		for calorie := range calories {
			calInt, _ := strconv.Atoi(calorie)
			sum += calInt
		}

		// Done with this sum
		if h.Len() < 3 {
			heap.Push(h, sum)
		} else if sum > (*h)[0] {
			heap.Pop(h)
			heap.Push(h, sum)
		}
	}

	totalCalories := 0
	for _, v := range *h {
		totalCalories += v
	}
	return totalCalories
}

type MinHeap []int

func (h MinHeap) Len() int           { return len(h) }
func (h MinHeap) Less(i, j int) bool { return h[i] < h[j] }
func (h MinHeap) Swap(i, j int)      { h[i], h[j] = h[j], h[i] }
func (h *MinHeap) Push(x any)        { *h = append(*h, x.(int)) }
func (h *MinHeap) Pop() any {
	old := *h
	n := len(old)
	x := old[n-1]
	*h = old[0 : n-1]
	return x
}
