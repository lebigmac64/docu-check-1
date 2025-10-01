#!/usr/bin/env bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Building backend..."
cd "$SCRIPT_DIR/backend"
dotnet test --configuration Release

echo "Building frontend..."
cd "$SCRIPT_DIR/frontend/docucheck-web"

vite build

echo "Build completed."