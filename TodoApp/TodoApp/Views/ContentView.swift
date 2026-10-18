//
//  ContentView.swift
//  TodoApp
//
//  Created by luka khujadze on 17/10/2026.
//

import SwiftUI

struct TodoTask: Identifiable, Codable {
    let id: Int
    let title: String
    var isCompleted: Bool
    let categoryId: Int?
}


struct TodoAPI {
    let baseURLString: String
    
    func loadTodos() async throws -> [TodoTask] {
        guard let baseURL = URL(string: baseURLString) else {
            throw URLError(.badURL)
        }
        
        let url = baseURL.appendingPathComponent("api/todo")
        
        let (data, response) = try await URLSession.shared.data(from: url)
        
        guard let httpResponse = response as? HTTPURLResponse else {
            throw URLError(.badServerResponse)
        }
        
        guard (200...299).contains(httpResponse.statusCode) else {
            throw URLError(.badServerResponse)
        }
        
        return try JSONDecoder().decode([TodoTask].self, from: data)
    }
}


struct ContentView: View {
    @State private var isShowingAddTask = false
    @State private var newTaskTitle = ""
    @State private var tasks: [TodoTask] = []
    
    private let api = TodoAPI(
        baseURLString: "https://sudoku-ungraded-nemeses.ngrok-free.dev"
    )
    
    var body: some View {
        ZStack {
            Color("background")
                .ignoresSafeArea()
            
            VStack(alignment: .leading, spacing: 20) {
                
                HStack(spacing: 102) {
                    
                    Text("My Tasks")
                        .font(.largeTitle.bold())
                        .foregroundStyle(.white)
                        .padding()
                    
                    Button(action: {
                        isShowingAddTask = true
                        print("Added")
                    }) {
                        Image(systemName: "plus")
                            .font(.title2.bold())
                            .foregroundColor(.white)
                            .frame(width: 52, height: 52)
                            .background(Color.blue.gradient)
                            .clipShape(Circle())
                    }
                    .padding()
                    
                }
                
                Divider()
                    .frame(height: 1)
                    .overlay(Color.white)
                
                List {
                    ForEach($tasks) { $task in
                        TodoTaskRow(task: $task)
                            .swipeActions {
                                Button(role: .destructive) {
                                    deleteTask(id: task.id)
                                } label: {
                                    Label("Delete", systemImage: "trash")
                                }
                            }
                    }
                }
                .listStyle(.plain)
                .scrollContentBackground(.hidden)
            }
            .padding()
            .frame(maxWidth: .infinity, maxHeight: .infinity, alignment: .topLeading)
        }
        .sheet(isPresented: $isShowingAddTask) {
            NavigationStack {
                Form {
                    TextField("Task title", text: $newTaskTitle)
                }
                .navigationTitle("New Task")
                .toolbar {
                    ToolbarItem(placement: .cancellationAction) {
                        Button("Cancel") {
                            newTaskTitle = ""
                            isShowingAddTask = false
                        }
                    }
                    
                    ToolbarItem(placement: .confirmationAction) {
                        Button("Add") {
                            let trimmedTitle = newTaskTitle.trimmingCharacters(in: .whitespacesAndNewlines)
                            
                            guard !trimmedTitle.isEmpty else { return }
                            
                            tasks.append(
                                TodoTask(
                                    id: -Int.random(in: 1...Int.max),
                                    title: trimmedTitle,
                                    isCompleted: false,
                                    categoryId: 1
                                )
                            )
                            newTaskTitle = ""
                            isShowingAddTask = false
                        }
                    }
                }
            }
        }
        .task {
            await loadTasks()
        }
    }
    private func loadTasks() async {
        do {
            tasks = try await api.loadTodos()
        } catch {
            print("Could not load tasks: \(error.localizedDescription)")
        }
    }
    
    private func deleteTask(id: TodoTask.ID) {
        tasks.removeAll { $0.id == id }
    }
}

struct TodoTaskRow: View {
    @Binding var task: TodoTask
    
    var body: some View {
        HStack(spacing: 12) {
            Button {
                task.isCompleted.toggle()
            } label: {
                Image(systemName: task.isCompleted ? "checkmark.circle.fill" : "circle")
                    .font(.title2)
                    .foregroundStyle(task.isCompleted ? .green : .white)
            }
            .buttonStyle(.plain)
            .accessibilityLabel(
                task.isCompleted
                ? "Mark \(task.title) as incomplete"
                : "Mark \(task.title) as complete"
            )
            
            Text(task.title)
                .foregroundStyle((task.isCompleted ? Color.secondary : Color.white))
                .strikethrough(task.isCompleted)
            
            Spacer()
        }
        .padding(.vertical, 6)
        .listRowBackground(Color.clear)
    }
}


#Preview {
    ContentView()
}
