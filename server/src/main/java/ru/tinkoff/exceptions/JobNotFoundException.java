package ru.tinkoff.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(HttpStatus.NOT_FOUND)
public final class JobNotFoundException extends RuntimeException {
    public JobNotFoundException(String code) {
        super(String.format("Job with code '%s' not found.", code));
    }
}
