import reactLogo from './assets/react.svg';
import viteLogo from '/vite.svg';
import './App.css';
import React, { useState } from "react";
import styles from "./TodoApp.module.css";

interface Todo {
  text: string;
  completed: boolean;
}

function App() {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [input, setInput] = useState<string>("");

  const handleAddTodo = () => {
    if (!input.trim()) return;
    setTodos([...todos, { text: input, completed: false }]);
    setInput("");
  };

  const handleToggleTodo = (index: number) => {
    setTodos(
      todos.map((todo, i) =>
        i === index ? { ...todo, completed: !todo.completed } : todo
      )
    );
  };

  const handleRemoveTodo = (index: number) => {
    setTodos(todos.filter((_, i) => i !== index));
  };

  return (
    <div className={styles.container}>
      <h2 className={styles.header}>Todo List</h2>
      <div className={styles.inputRow}>
        <input
          className={styles.input}
          type="text"
          value={input}
          onChange={e => setInput(e.target.value)}
          placeholder="Enter todo"
        />
        <button
          className={styles.btn}
          onClick={handleAddTodo}
          disabled={!input.trim()}
        >
          Add
        </button>
      </div>
      <ul className={styles.todoList}>
        {todos.map((todo, idx) => (
          <li key={idx} className={styles.todoItem}>
            <span
              className={todo.completed ? styles.completed : ""}
              onClick={() => handleToggleTodo(idx)}
              style={{ cursor: "pointer" }}
              title="Toggle completion"
            >
              {todo.text}
            </span>
            <button
              className={styles.removeBtn}
              onClick={() => handleRemoveTodo(idx)}
              title="Remove"
            >
              &times;
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
