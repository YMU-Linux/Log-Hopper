// Simple client-side form handler (demo only)
document.getElementById('loginForm').addEventListener('submit', function (e) {
  e.preventDefault();
  const user = document.getElementById('usernameOrEmail').value;
  const pass = document.getElementById('password').value;

  alert(`Logging in with:\nUsername/Email: ${user}\nPassword: ${pass}`);
  // TODO: Replace alert with real login API call
});

// Simple handler for forgot password form
document.getElementById('forgotPasswordForm').addEventListener('submit', function (e) {
  e.preventDefault();
  const userOrEmail = document.getElementById('usernameOrEmail').value;

  alert(`Password reset link has been sent to: ${userOrEmail}`);
  // TODO: Replace alert with real backend API call
});
