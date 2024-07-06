
public static class Priority {
    public static void Test() {
        // Test Cases

        // Test 1
        // Scenario: Enqueue three items with different priorities and dequeue them.
        // Expected Result: Dequeue returns the item with the highest priority each time.
        Console.WriteLine("Test 1");
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 3);
        priorityQueue.Enqueue("Item3", 2);

        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item2
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item3
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item1
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: The queue is empty.

        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 2
        // Scenario: Enqueue items with the same priority and ensure FIFO order.
        // Expected Result: Dequeue returns items in the order they were added.
        Console.WriteLine("Test 2");
        priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 2);
        priorityQueue.Enqueue("Item2", 2);
        priorityQueue.Enqueue("Item3", 2);

        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item1
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item2
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item3
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: The queue is empty.

        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 3
        // Scenario: Dequeue from an empty queue.
        // Expected Result: Error message "The queue is empty."
        Console.WriteLine("Test 3");
        priorityQueue = new PriorityQueue();
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: The queue is empty.

        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 4
        // Scenario: Enqueue multiple items with various priorities and ensure correct order.
        // Expected Result: Dequeue returns items in correct priority order.
        Console.WriteLine("Test 4");
        priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("LowPriority", 1);
        priorityQueue.Enqueue("HighPriority", 10);
        priorityQueue.Enqueue("MediumPriority", 5);

        Console.WriteLine(priorityQueue.Dequeue()); // Expected: HighPriority
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: MediumPriority
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: LowPriority
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: The queue is empty.

        // Defect(s) Found: None

        Console.WriteLine("---------");

        // Test 5
        // Scenario: Enqueue items with mixed priorities and dequeue interleaved.
        // Expected Result: Correct items should be dequeued based on priority.
        Console.WriteLine("Test 5");
        priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Item1", 1);
        priorityQueue.Enqueue("Item2", 3);
        priorityQueue.Enqueue("Item3", 2);
        priorityQueue.Enqueue("Item4", 3);

        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item2
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item4
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item3
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: Item1
        Console.WriteLine(priorityQueue.Dequeue()); // Expected: The queue is empty.

        // Defect(s) Found: None

        Console.WriteLine("---------");
    }
}
