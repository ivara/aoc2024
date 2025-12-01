# Day 01, 2022

## Part 2

Multiple ways to maintain top-k list. Min-Heap is the specific one that this problem is tailored around.

### Min-Heap

A Min Heap is a binary heap where the parent node is always smaller than or equal to its child nodes.

Widely used in priority queues.

Key Points

- The heap maintains the minimum at index 0
- For top-k largest, use a min-heap (counterintuitive but correct!)
- For top-k smallest, use a max-heap
- heap.Pop() removes the minimum and returns it
- Direct access [*h](0) peeks at minimum without removing
