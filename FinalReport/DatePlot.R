# Install packages if not already installed
# install.packages("dplyr")
# install.packages("readr")
# install.packages("ggplot2")

library(dplyr)
library(readr)
library(ggplot2)

# Read in the CSV files
ga_data <- read_csv("C:\Users\kettn\Documents\CS5600\FinalReport\RootSolvingMethod\RootSolvingMethod\bin\Debug\net6.0\ga_results.csv")            # columns: function, root, iterations, time_ms
bisection_data <- read_csv("C:\Users\kettn\Documents\CS5600\FinalReport\RootSolvingMethod\RootSolvingMethod\bin\Debug\net6.0\bisection_results.csv") 
newton_data <- read_csv("C:\Users\kettn\Documents\CS5600\FinalReport\RootSolvingMethod\RootSolvingMethod\bin\Debug\net6.0\newton_results.csv")

# Read the expected roots file
expected <- read_csv("C:\Users\kettn\Documents\CS5600\FinalReport\RootSolvingMethod\RootSolvingMethod\bin\Debug\net6.0\expected_roots.csv")       # columns: function, expected_root

# Add a method column to each dataset
ga_data <- ga_data %>% mutate(Method = "GA")
bisection_data <- bisection_data %>% mutate(Method = "Bisection")
newton_data <- newton_data %>% mutate(Method = "Newton-Raphson")

# Combine all results into a single dataframe
results <- bind_rows(ga_data, bisection_data, newton_data)

# Join with the expected roots to compare results
results <- left_join(results, expected, by = "function")

# Compute error (if expected_root is not NA)
results <- results %>%
  mutate(error = ifelse(!is.na(expected_root), abs(root - expected_root), NA))

# 1. Plot absolute error by method and function
ggplot(results, aes(x = function, y = error, fill = Method)) +
  geom_bar(stat = "identity", position = "dodge") +
  coord_flip() +
  theme_minimal() +
  labs(title = "Absolute Error by Method and Function",
       x = "Function",
       y = "Absolute Error")

# 2. Plot iterations by method and function
ggplot(results, aes(x = function, y = iterations, fill = Method)) +
  geom_bar(stat = "identity", position = "dodge") +
  coord_flip() +
  theme_minimal() +
  labs(title = "Number of Iterations by Method and Function",
       x = "Function",
       y = "Iterations")

# 3. Plot time by method and function
ggplot(results, aes(x = function, y = time_ms, fill = Method)) +
  geom_bar(stat = "identity", position = "dodge") +
  coord_flip() +
  theme_minimal() +
  labs(title = "Computation Time by Method and Function",
       x = "Function",
       y = "Time (ms)")

# 4. If you'd like to visualize how error relates to computation time, you can use a scatter plot
ggplot(results, aes(x = time_ms, y = error, color = Method)) +
  geom_point(size = 3) +
  theme_minimal() +
  labs(title = "Error vs. Time by Method",
       x = "Time (ms)",
       y = "Absolute Error")

# 5. If you have multiple runs or want to see distributions, you can use boxplots
ggplot(results, aes(x = Method, y = error, fill = Method)) +
  geom_boxplot() +
  theme_minimal() +
  labs(title = "Distribution of Errors by Method",
       x = "Method",
       y = "Absolute Error")

# Feel free to modify colors, themes, or plot types as desired.