W <- c(70, 75, 77, 80, 82, 84, 87, 90 )# Weight (independent variable)

A <- c(2.10, 2.12, 2.15, 2.20, 2.22, 2.23, 2.26, 2.30) # Area (dependent variable)


ln_W <- log(W)
ln_A <- log(A)

mean_ln_W <- mean(ln_W)
mean_ln_A <- mean(ln_A)

slope_b <- sum((ln_W - mean_ln_W) * (ln_A - mean_ln_A)) / sum((ln_W - mean_ln_W)^2)

intercept_ln_a <- mean_ln_A - slope_b * mean_ln_W

a <- exp(intercept_ln_a)

# Generate fitted values
fitted_A <- a * W^slope_b

# Plot the original data
plot(W, A, main = "Power-Law Fit", xlab = "Weight (W)", ylab = "Area (A)", pch = 16)
lines(W, fitted_A, col = "blue", lwd = 2) 

# Residuals
residuals <- A - fitted_A

# Print residuals
cat("Residuals:", residuals, "\n")


W2 <- 95

result <- a * W2^b

cat("Predict surface area for 95kg:", result, "\n")

RSS <- sum(residuals^2)

n <- length(W)  # Count of W

standardError <- sqrt(RSS / (n - 2))

# Print the standard error
cat("Standard Error:", standardError, "\n")