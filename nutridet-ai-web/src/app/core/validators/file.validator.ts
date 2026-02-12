// File validation constraints
export const FILE_CONSTRAINTS = {
  ALLOWED_TYPES: ['image/jpeg', 'image/jpg', 'image/png'],
  ALLOWED_EXTENSIONS: ['.jpg', '.jpeg', '.png'],
  MAX_SIZE: 10 * 1024 * 1024 // 10MB
} as const;

// Error messages
export const FILE_ERROR_MESSAGES = {
  NO_FILE: 'Vui lòng chọn file ảnh',
  INVALID_FILE_TYPE: 'Chỉ chấp nhận file ảnh: JPG, JPEG, PNG',
  FILE_TOO_LARGE: (maxMB: number, currentMB: string) => `Kích thước file quá lớn. Tối đa ${maxMB}MB. File hiện tại: ${currentMB}MB`,
  INVALID_MIME_TYPE: 'Định dạng file không hợp lệ. Vui lòng chọn file ảnh JPG hoặc PNG',
  READ_FILE_ERROR: 'Không thể đọc file ảnh. File có thể bị hỏng hoặc không hợp lệ.'
} as const;

// Validation result interface
export interface FileValidationResult {
  isValid: boolean;
  error: string | null;
}

/**
 * Validates a file based on constraints
 * @param file - File to validate
 * @param constraints - Optional custom constraints (uses default if not provided)
 * @returns Validation result with isValid flag and error message
 */
export function validateFile(
  file: File | null,
  constraints?: {
    allowedTypes?: readonly string[];
    allowedExtensions?: readonly string[];
    maxSize?: number;
  }
): FileValidationResult {
  // Use provided constraints or default
  const allowedTypes = constraints?.allowedTypes || FILE_CONSTRAINTS.ALLOWED_TYPES;
  const allowedExtensions = constraints?.allowedExtensions || FILE_CONSTRAINTS.ALLOWED_EXTENSIONS;
  const maxSize = constraints?.maxSize || FILE_CONSTRAINTS.MAX_SIZE;

  // Validate file exists
  if (!file) {
    return {
      isValid: false,
      error: FILE_ERROR_MESSAGES.NO_FILE
    };
  }

  // Validate file extension
  const fileExtension = '.' + file.name.split('.').pop()?.toLowerCase();
  if (!allowedExtensions.includes(fileExtension)) {
    return {
      isValid: false,
      error: FILE_ERROR_MESSAGES.INVALID_FILE_TYPE
    };
  }

  // Validate file size
  if (file.size > maxSize) {
    const maxSizeMB = maxSize / (1024 * 1024);
    const fileSizeMB = (file.size / (1024 * 1024)).toFixed(2);
    return {
      isValid: false,
      error: FILE_ERROR_MESSAGES.FILE_TOO_LARGE(maxSizeMB, fileSizeMB)
    };
  }

  // Validate MIME type
  if (file.type) {
    if (!allowedTypes.includes(file.type.toLowerCase())) {
      return {
        isValid: false,
        error: FILE_ERROR_MESSAGES.INVALID_MIME_TYPE
      };
    }
  }

  return {
    isValid: true,
    error: null
  };
}

