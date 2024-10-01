"""
Production settings for Paperboy project.
"""

import os
from dotenv import load_dotenv
from paperboy.settings.base import *  # noqa

# Load the .env file
load_dotenv()

SECRET_KEY = os.getenv("APP_SECRET_KEY", "")
DEBUG = False
ALLOWED_HOSTS = []

# Database
# https://docs.djangoproject.com/en/5.1/ref/settings/#databases

DATABASES = {
    "default": {
        "ENGINE": "django.db.backends.postgresql",
        "NAME": os.getenv("APP_DB_NAME", ""),
        "HOST": os.getenv("APP_DB_HOST", ""),
        "USER": os.getenv("APP_DB_USER", ""),
        "PASSWORD": os.getenv("APP_DB_PASSWORD", ""),
    }
}
