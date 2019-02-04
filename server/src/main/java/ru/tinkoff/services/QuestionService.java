package ru.tinkoff.services;

import ru.tinkoff.exceptions.JobNotFoundException;
import ru.tinkoff.models.*;
import ru.tinkoff.repositories.JobRepository;
import ru.tinkoff.repositories.QuestionRepository;

import java.util.Collection;

public final class QuestionService {
    private final QuestionRepository questionRepository;
    private final JobRepository jobRepository;

    public QuestionService() {
        this.questionRepository = new QuestionRepository();
        this.jobRepository = new JobRepository();
    }

    public Collection<Question> getAll(String jobCode) {
        if (this.jobRepository.getJobByCode(jobCode) == null) {
            throw new JobNotFoundException(jobCode);
        }

        return this.questionRepository.getByJobCode(jobCode);
    }
}
