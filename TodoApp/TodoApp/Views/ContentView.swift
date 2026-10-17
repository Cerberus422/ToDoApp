//
//  ContentView.swift
//  TodoApp
//
//  Created by luka khujadze on 17/10/2026.
//

import SwiftUI

struct ContentView: View {
    @State private var isTaskAdded = false
    
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
                        isTaskAdded = true
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
                
                ScrollView {
                    VStack(alignment: .leading, spacing: 12) {
                        Text("Tasks go here!!!")
                            .foregroundStyle(.white)
                            .padding()
                    }
                    .frame(maxWidth: .infinity, alignment: .leading)
                }
            }
            .padding()
            .frame(maxWidth: .infinity, maxHeight: .infinity, alignment: .topLeading)
        }
    }
}

#Preview {
    ContentView()
}
