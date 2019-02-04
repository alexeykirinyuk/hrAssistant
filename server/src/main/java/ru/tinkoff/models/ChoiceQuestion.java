package ru.tinkoff.models;

import ru.tinkoff.helpers.Guard;

import java.util.Collection;

public final class ChoiceQuestion extends Question {
    private final Collection<Option> options;

    public ChoiceQuestion(String title, boolean required, Collection<Option> options) {
        super(title, required);

        Guard.IsNotNull(options, "options");
        this.options = options;
    }

    @Override
    public QuestionType getQuestionType() {
        return QuestionType.Choice;
    }

    public Collection<Option> getOptions() {
        return options;
    }
}
